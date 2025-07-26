import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <header class="header">
        <h1>Image Upload & Crop Application</h1>
        <p>Upload, crop, and save images with custom fields</p>
      </header>
      
      <main class="main-content">
        <app-image-upload></app-image-upload>
      </main>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 20px;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
    
    .header {
      text-align: center;
      margin-bottom: 40px;
      padding: 20px;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
      border-radius: 10px;
      box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }
    
    .header h1 {
      margin: 0 0 10px 0;
      font-size: 2.5rem;
      font-weight: 300;
    }
    
    .header p {
      margin: 0;
      font-size: 1.1rem;
      opacity: 0.9;
    }
    
    .main-content {
      background: white;
      border-radius: 10px;
      box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
      overflow: hidden;
    }
  `]
})
export class AppComponent {
  title = 'Image Upload App';
}