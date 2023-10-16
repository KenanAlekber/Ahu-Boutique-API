//using Microsoft.AspNetCore.Mvc;

//namespace Ahu.API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class FileUploadController : ControllerBase
//{
//    private readonly IWebHostEnvironment _webHostEnvironment;

//    public FileUploadController(IWebHostEnvironment webHostEnvironment)
//    {
//        _webHostEnvironment = webHostEnvironment;
//    }

//    [HttpPost("upload")]
//    public async Task<IActionResult> Upload(IFormFile file)
//    {
//        if (file is null || file.Length == 0)
//            return BadRequest();

//        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Files", file.FileName);
//        using (var stream = new FileStream(filePath, FileMode.Create))
//        {
//            await file.CopyToAsync(stream);
//        }

//        return Ok("File upload successfully");
//    }
//}