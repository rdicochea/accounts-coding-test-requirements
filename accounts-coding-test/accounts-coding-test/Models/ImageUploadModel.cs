using System;
using System.ComponentModel.DataAnnotations;

namespace accounts_coding_test.Models
{
    public class ImageUploadModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [StringLength(255)]
        public string ImageFileName { get; set; }
        
        public string ImageFilePath { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedDate { get; set; }
    }
    
    public class ImageUploadRequest
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public string ImageData { get; set; } // Base64 encoded image data
        public string ImageFileName { get; set; }
        public int CropX { get; set; }
        public int CropY { get; set; }
        public int CropWidth { get; set; }
        public int CropHeight { get; set; }
        public int TargetWidth { get; set; } = 800;
        public int TargetHeight { get; set; } = 600;
    }
}