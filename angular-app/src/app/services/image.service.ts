import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ImageUploadRequest {
  title: string;
  description: string;
  imageData: string;
  imageFileName: string;
  cropX: number;
  cropY: number;
  cropWidth: number;
  cropHeight: number;
  targetWidth: number;
  targetHeight: number;
}

export interface ImageUploadModel {
  id: number;
  title: string;
  description: string;
  imageFileName: string;
  imageFilePath: string;
  createdDate: string;
  updatedDate?: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data?: T;
}

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private apiUrl = 'https://localhost:5001/api/imageupload'; // Update with your actual API URL

  constructor(private http: HttpClient) { }

  uploadImage(request: ImageUploadRequest): Observable<ApiResponse<ImageUploadModel>> {
    return this.http.post<ApiResponse<ImageUploadModel>>(`${this.apiUrl}/upload`, request);
  }

  getImageList(): Observable<ApiResponse<ImageUploadModel[]>> {
    return this.http.get<ApiResponse<ImageUploadModel[]>>(`${this.apiUrl}/list`);
  }

  getImage(id: number): Observable<ApiResponse<ImageUploadModel>> {
    return this.http.get<ApiResponse<ImageUploadModel>>(`${this.apiUrl}/${id}`);
  }
}