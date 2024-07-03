namespace Dll.Entity
{
	public class QRCodeModel
	{
		public int Id { get; set; }
		public string? QRText { get; set; }
		public DateTime DateOfCreate { get; set; } = DateTime.Now;
	}
}
