using AutoMapper;
using Bll.Interfaces_Repository;
using Dll.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PllDoctor.DTO;
using PllDoctor.Models;

namespace PllDoctor.Controllers
{
    [Authorize(Roles = "Doctor,Admin")]

    public class SubjectController : Controller
    {

        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SubjectController> _logger;



        public SubjectController(IUniteOfWork uniteOfWork, IMapper mapper, ILogger<SubjectController> logger)
        {

            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
            _logger = logger;

        }

        public IActionResult GetAllSubjects(string? SubjectName)
        {
            if (User.IsInRole("Doctor") || User.IsInRole("Admin"))
            {
                // IEnumerable<SubjectViewModel> subject;
                if (string.IsNullOrEmpty(SubjectName))
                {
                    var subjects = _uniteOfWork.SubjectRepository.GetAll();
                    var mapped = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectViewModel>>(subjects);
                    return View(mapped);
                }
                else

                {
                    var subjects = _uniteOfWork.SubjectRepository.SearchByName(SubjectName);
                    var mapped = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectViewModel>>(subjects);
                    return View(mapped);
                }
            }

            return RedirectToAction(nameof(AccessDenied));
        }
        [HttpGet]
        public IActionResult AddSubjects()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddSubjects(SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedSubject = _mapper.Map<SubjectViewModel, Subject>(subject);
                    _uniteOfWork.SubjectRepository.Add(MappedSubject);
                    int result = _uniteOfWork.Complete();
                    if (result > 0)
                        return RedirectToAction("GetAllSubjects");
                    else
                        return BadRequest();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(subject);
        }
        public IActionResult DeleteSubjects(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id is null)
                    return View("Error");

                var sub = _uniteOfWork.SubjectRepository.GetbyId(id.Value);
                _uniteOfWork.SubjectRepository.Delete(sub);
                int result = _uniteOfWork.Complete();
                if (result > 0)
                {
                    TempData["Removed"] = "Subject deleted Successfully ..!";
                }
                else
                    return BadRequest();
                return RedirectToAction("GetAllSubjects");
            }
            return RedirectToAction(nameof(AccessDenied));
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (User.IsInRole("Admin"))
            {
                if (id is null)
                    return BadRequest();

                var subject = _uniteOfWork.SubjectRepository.GetbyId(id.Value);
                if (subject is null)
                    return NotFound();

                return View(subject);
            }
            else
                return RedirectToAction(nameof(AccessDenied));
        }
        [HttpPost]

        public IActionResult Update(Subject subject, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uniteOfWork.SubjectRepository.Update(subject);
                    int result = _uniteOfWork.Complete();
                    if (result > 0)
                    {
                        TempData["Updated"] = $"{subject.Name} Updated Successfully ..!";
                        return RedirectToAction("GetAllSubjects");
                    }
                    else
                        return BadRequest();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(subject);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }

}
