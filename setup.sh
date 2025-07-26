#!/bin/bash

echo "🚀 Setting up Image Upload & Crop Application"
echo "=============================================="

# Check if .NET Core is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET Core SDK is not installed. Please install .NET Core 3.1 or later."
    exit 1
fi

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo "❌ Node.js is not installed. Please install Node.js 16 or later."
    exit 1
fi

# Check if npm is installed
if ! command -v npm &> /dev/null; then
    echo "❌ npm is not installed. Please install npm."
    exit 1
fi

echo "✅ Prerequisites check passed"

# Setup Backend
echo ""
echo "🔧 Setting up C# Backend..."
cd accounts-coding-test/accounts-coding-test

echo "📦 Restoring NuGet packages..."
dotnet restore

if [ $? -eq 0 ]; then
    echo "✅ Backend dependencies restored"
else
    echo "❌ Failed to restore backend dependencies"
    exit 1
fi

# Create uploads directory
mkdir -p wwwroot/uploads
echo "✅ Created uploads directory"

cd ../../

# Setup Frontend
echo ""
echo "🔧 Setting up Angular Frontend..."
cd angular-app

echo "📦 Installing npm packages..."
npm install

if [ $? -eq 0 ]; then
    echo "✅ Frontend dependencies installed"
else
    echo "❌ Failed to install frontend dependencies"
    exit 1
fi

cd ../

echo ""
echo "🎉 Setup completed successfully!"
echo ""
echo "📋 Next steps:"
echo "1. Start the backend:"
echo "   cd accounts-coding-test/accounts-coding-test"
echo "   dotnet run"
echo ""
echo "2. In a new terminal, start the frontend:"
echo "   cd angular-app"
echo "   ng serve"
echo ""
echo "3. Open your browser and navigate to:"
echo "   http://localhost:4200"
echo ""
echo "4. Test the API endpoint:"
echo "   https://localhost:5001/api/imageupload/list"
echo ""
echo "Happy coding! 🚀"