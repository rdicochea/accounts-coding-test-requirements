using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using accounts_coding_test.Models;
using System.Collections.Generic;

namespace accounts_coding_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _uploadPath;
        private static List<ImageUploadModel> _imageData = new List<ImageUploadModel>();
        private static int _nextId = 1;

        public ImageUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            // Ensure upload directory exists
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpPost("upload")]
        public IActionResult UploadImage([FromBody] ImageUploadRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ImageData))
                {
                    return BadRequest("Image data is required");
                }

                // Remove data URL prefix if present
                var base64Data = request.ImageData;
                if (base64Data.Contains(","))
                {
                    base64Data = base64Data.Split(',')[1];
                }

                // Decode base64 image
                var imageBytes = Convert.FromBase64String(base64Data);
                
                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}_{request.ImageFileName ?? "image.jpg"}";
                var filePath = Path.Combine(_uploadPath, fileName);

                // Process and save the image
                using (var originalImage = Image.FromStream(new MemoryStream(imageBytes)))
                {
                    // Create target image with fixed dimensions
                    using (var targetImage = new Bitmap(request.TargetWidth, request.TargetHeight))
                    {
                        using (var graphics = Graphics.FromImage(targetImage))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Calculate scaling to fit the image within target dimensions
                            var scaleX = (double)request.TargetWidth / request.CropWidth;
                            var scaleY = (double)request.TargetHeight / request.CropHeight;
                            var scale = Math.Max(scaleX, scaleY);

                            var scaledWidth = (int)(request.CropWidth * scale);
                            var scaledHeight = (int)(request.CropHeight * scale);

                            // Center the image
                            var x = (request.TargetWidth - scaledWidth) / 2;
                            var y = (request.TargetHeight - scaledHeight) / 2;

                            // Draw the cropped and scaled image
                            var sourceRect = new Rectangle(request.CropX, request.CropY, request.CropWidth, request.CropHeight);
                            var destRect = new Rectangle(x, y, scaledWidth, scaledHeight);

                            graphics.DrawImage(originalImage, destRect, sourceRect, GraphicsUnit.Pixel);
                        }

                        // Save the processed image
                        targetImage.Save(filePath, ImageFormat.Jpeg);
                    }
                }

                // Create database record
                var imageRecord = new ImageUploadModel
                {
                    Id = _nextId++,
                    Title = request.Title,
                    Description = request.Description,
                    ImageFileName = fileName,
                    ImageFilePath = $"/uploads/{fileName}",
                    CreatedDate = DateTime.Now
                };

                _imageData.Add(imageRecord);

                return Ok(new { 
                    success = true, 
                    message = "Image uploaded successfully",
                    data = imageRecord
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error uploading image: {ex.Message}" });
            }
        }

        [HttpGet("list")]
        public IActionResult GetImageList()
        {
            try
            {
                return Ok(new { success = true, data = _imageData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error retrieving images: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            try
            {
                var image = _imageData.FirstOrDefault(x => x.Id == id);
                if (image == null)
                {
                    return NotFound(new { success = false, message = "Image not found" });
                }

                return Ok(new { success = true, data = image });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error retrieving image: {ex.Message}" });
            }
        }
    }
}