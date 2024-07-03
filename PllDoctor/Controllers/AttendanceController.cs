using AutoMapper;
using Bll.Interfaces_Repository;
using ClosedXML.Excel;
using Dll.DataContext;
using Dll.Entity;
//using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PllDoctor.DTO;
using PllDoctor.Models;
using System.Data;

namespace PllDoctor.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {

        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        private readonly SystemDbContext _dbContext;

        public AttendanceController(IUniteOfWork uniteOfWork, IMapper mapper, SystemDbContext dbContext)
        {

            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateQRCode(string Name)
        {
            string name = Name;
            if (name is not null)
            {
                string[] arr = QRCodeGeneratorService.GenerateQRCode(_uniteOfWork, name);
                #region For View
                ViewBag.QrCodeUri0 = arr[0];
                ViewBag.QrCodeUri1 = arr[1];
                ViewBag.QrCodeUri2 = arr[2];
                #endregion 

            }
            return View();
        }
        //Show Attendance
        [HttpPost]
        public IActionResult GetAttendance(string SubjectName, DateTime? dateTime)
        {
            if (!string.IsNullOrEmpty(SubjectName))  //get All Attendance subject
            {
                IQueryable<AttendanceTable> CustomAttendance;
                TempData["subject"] = SubjectName;

                if (dateTime is null)
                {
                    CustomAttendance = _uniteOfWork.AttendanceRepository.GetRequiredAttendance(SubjectName, DateTime.Today);//To get Attendance For This day 
                }
                else { CustomAttendance = _uniteOfWork.AttendanceRepository.GetRequiredAttendance(SubjectName, dateTime); }
                if (CustomAttendance != null)
                {
                    var mappedAttendance = _mapper.Map<IEnumerable<AttendanceTable>, IEnumerable<AttendanceModelDTO>>(CustomAttendance);
                    return View(mappedAttendance);
                }
                else
                    return BadRequest();
            }
            else
            {
                var Attendance = _uniteOfWork.AttendanceRepository.GetAll();
                var mappedAttendance = _mapper.Map<IEnumerable<AttendanceTable>, IEnumerable<AttendanceModelDTO>>(Attendance);
                return View(mappedAttendance);
            }

        }

        [HttpPost]
        public FileResult ExportFile(string SubjectName)
        {
            IQueryable<AttendanceTable> CustomAttendance;
            CustomAttendance = _uniteOfWork.AttendanceRepository.GetRequiredAttendance(SubjectName, DateTime.Today);

            var Attendance = _mapper.Map<IEnumerable<AttendanceTable>, IEnumerable<AttendanceModelDTO>>(CustomAttendance);

            var FileName = "Attendance.xlsx";
            return GenerateExcel(FileName, Attendance);


        }

        private FileResult GenerateExcel(string FileName, IEnumerable<AttendanceModelDTO> attendanceModels)
        {
            DataTable dataTable = new DataTable("attendanceModels");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Student Name"),
                new DataColumn("Student ID")
            });
            foreach (var item in attendanceModels)
            {
                dataTable.Rows.Add(item.StudentName, item.STDId);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(AttendanceModelDTO attendanceModel)
        {
            if (ModelState.IsValid)
            {
                var mapped = _mapper.Map<AttendanceModelDTO, AttendanceTable>(attendanceModel);
                _uniteOfWork.AttendanceRepository.Add(mapped);
                var result = _uniteOfWork.Complete();
                if (result > 0)
                    return RedirectToAction("GetAllSubjects", "Subject");
                return View(mapped);

            }
            return View(attendanceModel);
        }

        [HttpDelete]
        public async Task DeleteQRAsync()
        {
            var lastRow = await _dbContext.QRCodesTable
                                     .OrderByDescending(x => x.Id)
                                     .FirstOrDefaultAsync();
            if (lastRow != null)
            {
                // Remove the last row
                _dbContext.QRCodesTable.Remove(lastRow);
                _uniteOfWork.Complete(); // Commit changes to the database
            }
        }

        public IActionResult TheEnd()
        {
            return View();
        }

    }
}
