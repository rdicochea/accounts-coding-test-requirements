# Image Upload & Crop Application

A full-stack web application that allows users to upload images, crop them to fit a fixed template size, and save them to a database with metadata. Built with Angular frontend and C# ASP.NET Core backend.

## Features

- **Image Upload**: Drag and drop or click to select images
- **Image Cropping**: Interactive cropping with fixed aspect ratio (800x600px)
- **Image Processing**: Automatic resizing and optimization
- **Form Validation**: Real-time validation for title and description fields
- **Modern UI**: Responsive design with beautiful animations
- **Image Gallery**: View all uploaded images with metadata
- **API Integration**: RESTful API with proper error handling

## Technology Stack

### Backend (C#)
- ASP.NET Core 3.1
- Entity Framework (in-memory for demo)
- System.Drawing.Common for image processing
- RESTful API design

### Frontend (Angular)
- Angular 15
- Reactive Forms
- ngx-image-cropper for image manipulation
- Modern CSS with responsive design
- TypeScript for type safety

## Project Structure

```
├── accounts-coding-test/          # C# Backend
│   └── accounts-coding-test/
│       ├── Controllers/
│       │   └── ImageUploadController.cs
│       ├── Models/
│       │   └── ImageUploadModel.cs
│       ├── wwwroot/
│       │   └── uploads/          # Image storage
│       └── accounts-coding-test.csproj
├── angular-app/                   # Angular Frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   └── image-upload/
│   │   │   ├── services/
│   │   │   │   └── image.service.ts
│   │   │   ├── app.component.ts
│   │   │   └── app.module.ts
│   │   ├── index.html
│   │   ├── main.ts
│   │   └── styles.css
│   ├── package.json
│   └── angular.json
└── README.md
```

## Setup Instructions

### Prerequisites

- .NET Core 3.1 SDK
- Node.js 16+ and npm
- Angular CLI (`npm install -g @angular/cli`)

### Backend Setup

1. **Navigate to the C# project:**
   ```bash
   cd accounts-coding-test/accounts-coding-test
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Verify the API is running:**
   - Open https://localhost:5001/api/imageupload/list
   - You should see an empty array `{"success":true,"data":[]}`

### Frontend Setup

1. **Navigate to the Angular project:**
   ```bash
   cd angular-app
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   ng serve
   ```

4. **Open the application:**
   - Navigate to http://localhost:4200
   - The application should load with the image upload interface

## API Endpoints

### POST /api/imageupload/upload
Upload and process an image with metadata.

**Request Body:**
```json
{
  "title": "My Image",
  "description": "Image description",
  "imageData": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQ...",
  "imageFileName": "photo.jpg",
  "cropX": 0,
  "cropY": 0,
  "cropWidth": 800,
  "cropHeight": 600,
  "targetWidth": 800,
  "targetHeight": 600
}
```

**Response:**
```json
{
  "success": true,
  "message": "Image uploaded successfully",
  "data": {
    "id": 1,
    "title": "My Image",
    "description": "Image description",
    "imageFileName": "guid_photo.jpg",
    "imageFilePath": "/uploads/guid_photo.jpg",
    "createdDate": "2024-01-15T10:30:00"
  }
}
```

### GET /api/imageupload/list
Get all uploaded images.

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "My Image",
      "description": "Image description",
      "imageFileName": "guid_photo.jpg",
      "imageFilePath": "/uploads/guid_photo.jpg",
      "createdDate": "2024-01-15T10:30:00"
    }
  ]
}
```

### GET /api/imageupload/{id}
Get a specific image by ID.

## Usage

1. **Upload an Image:**
   - Fill in the title and description fields
   - Click the upload area or drag and drop an image
   - Use the cropping tool to adjust the image
   - Click "Upload Image" to save

2. **View Uploaded Images:**
   - Click "Show Images" to view the gallery
   - Images are displayed with metadata
   - Click on images to view full size

3. **Image Processing:**
   - Images are automatically resized to 800x600px
   - Cropping maintains aspect ratio
   - Files are saved as JPEG format
   - Unique filenames prevent conflicts

## Configuration

### Image Template Size
The fixed template size is configured in the Angular component:
```typescript
readonly TARGET_WIDTH = 800;
readonly TARGET_HEIGHT = 600;
```

### File Storage
Images are stored in the `wwwroot/uploads/` directory. In production, consider:
- Using cloud storage (Azure Blob, AWS S3)
- Implementing file size limits
- Adding file type validation
- Setting up proper backup strategies

### Database
This demo uses in-memory storage. For production:
- Add Entity Framework with a real database
- Implement proper data models
- Add migration scripts
- Set up connection strings

## Development

### Adding New Features
1. **Backend:** Add new endpoints to `ImageUploadController.cs`
2. **Frontend:** Create new components and services
3. **Styling:** Update CSS files for new UI elements

### Testing
- Backend: Use `dotnet test` for unit tests
- Frontend: Use `ng test` for component tests
- API: Test endpoints with Postman or similar tools

## Troubleshooting

### Common Issues

1. **CORS Errors:**
   - Ensure the backend is running on the correct port
   - Check that the API URL in `image.service.ts` matches your backend

2. **Image Upload Fails:**
   - Verify the uploads directory exists
   - Check file permissions
   - Ensure image format is supported

3. **Angular Build Errors:**
   - Clear node_modules and reinstall: `rm -rf node_modules && npm install`
   - Update Angular CLI: `npm update -g @angular/cli`

4. **Backend Build Errors:**
   - Restore packages: `dotnet restore`
   - Clean and rebuild: `dotnet clean && dotnet build`

## Production Deployment

### Backend Deployment
1. Build the application: `dotnet publish -c Release`
2. Deploy to Azure App Service, AWS, or your preferred hosting
3. Configure environment variables for database connections
4. Set up proper file storage (cloud storage recommended)

### Frontend Deployment
1. Build for production: `ng build --prod`
2. Deploy the `dist/` folder to your web server
3. Configure API endpoints for production URLs
4. Set up HTTPS and proper security headers

## License

This project is provided as-is for educational and demonstration purposes.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## Support

For issues and questions:
1. Check the troubleshooting section
2. Review the API documentation
3. Examine the console logs for errors
4. Verify all dependencies are installed correctly
