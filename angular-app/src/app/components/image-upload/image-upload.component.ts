import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { ImageService, ImageUploadRequest, ImageUploadModel } from '../../services/image.service';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {
  uploadForm: FormGroup;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  showCropper = false;
  isUploading = false;
  uploadSuccess = false;
  uploadError = '';
  uploadedImages: ImageUploadModel[] = [];
  showImageList = false;

  // Fixed template dimensions
  readonly TARGET_WIDTH = 800;
  readonly TARGET_HEIGHT = 600;

  constructor(
    private fb: FormBuilder,
    private imageService: ImageService
  ) {
    this.uploadForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  ngOnInit(): void {
    this.loadImageList();
  }

  onFileSelected(event: any): void {
    this.imageChangedEvent = event;
    this.showCropper = true;
    this.croppedImage = '';
    this.uploadError = '';
  }

  imageCropped(event: ImageCroppedEvent): void {
    this.croppedImage = event.base64;
  }

  imageLoaded(): void {
    // Image loaded successfully
  }

  cropperReady(): void {
    // Cropper ready
  }

  loadImageFailed(): void {
    this.uploadError = 'Failed to load image. Please try again.';
    this.showCropper = false;
  }

  resetImage(): void {
    this.imageChangedEvent = '';
    this.croppedImage = '';
    this.showCropper = false;
    this.uploadError = '';
  }

  onSubmit(): void {
    if (this.uploadForm.valid && this.croppedImage) {
      this.isUploading = true;
      this.uploadError = '';

      const formData = this.uploadForm.value;
      const file = this.imageChangedEvent.target.files[0];

      const request: ImageUploadRequest = {
        title: formData.title,
        description: formData.description,
        imageData: this.croppedImage,
        imageFileName: file.name,
        cropX: 0, // These will be set by the cropper
        cropY: 0,
        cropWidth: this.TARGET_WIDTH,
        cropHeight: this.TARGET_HEIGHT,
        targetWidth: this.TARGET_WIDTH,
        targetHeight: this.TARGET_HEIGHT
      };

      this.imageService.uploadImage(request).subscribe({
        next: (response) => {
          if (response.success) {
            this.uploadSuccess = true;
            this.uploadedImages.unshift(response.data!);
            this.resetForm();
            this.loadImageList();
          } else {
            this.uploadError = response.message || 'Upload failed';
          }
        },
        error: (error) => {
          this.uploadError = 'An error occurred during upload. Please try again.';
          console.error('Upload error:', error);
        },
        complete: () => {
          this.isUploading = false;
        }
      });
    } else {
      this.uploadError = 'Please fill in all required fields and select an image.';
    }
  }

  resetForm(): void {
    this.uploadForm.reset();
    this.resetImage();
    this.uploadSuccess = false;
    this.uploadError = '';
  }

  loadImageList(): void {
    this.imageService.getImageList().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.uploadedImages = response.data;
        }
      },
      error: (error) => {
        console.error('Error loading image list:', error);
      }
    });
  }

  toggleImageList(): void {
    this.showImageList = !this.showImageList;
  }

  getImageUrl(imagePath: string): string {
    return `https://localhost:5001${imagePath}`;
  }
}