# Scolly - Course Management Web Application

Scolly is a web application designed to manage courses in a private school setting. It allows parents to register their children for courses, teachers to oversee student progress, and admins to manage all aspects of the system.
![image](https://github.com/user-attachments/assets/ff682d3d-4f4f-422c-95ac-a3b0fc619d47)

## 📌 Features

- **User roles**: Admin, Teachers, Parents
- **Parent-child account linking**
- **Course creation & enrollment management**
- **Teacher assignments & specialties**
- **Admin control over registrations and system data**

## 🚀 Technologies Used

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: .NET (C#)
- **Database**: SQL Server

## 📖 User Roles & Permissions

### 👨‍💼 Admin

Admins have full control over the system and can:
- Manage categories:
  - Age Groups (for course classification)
  - Cities (linked to parents and teachers)
  - Course Types (to define courses)
  - Specialties (to categorize teachers)
![image](https://github.com/user-attachments/assets/c60c4864-868d-4882-9012-9a9d4ce23282)
- Create and manage courses (type, duration, price, description, etc.)
![image](https://github.com/user-attachments/assets/bd90a483-9cea-42f9-b7e1-729bef1ba4e9)
![image](https://github.com/user-attachments/assets/85490a23-6530-4129-86b1-78e434206073)
![image](https://github.com/user-attachments/assets/144d9c4d-c077-434e-81de-3956ad2cf1cc)
- Assign teachers to courses
- Manage teacher registrations
![image](https://github.com/user-attachments/assets/1a244c2e-87ca-4587-80d7-8c1e56727ae0)
- Approve or reject parent requests to enroll children in courses
![image](https://github.com/user-attachments/assets/2569bd8b-8876-4b16-b201-5c0964b57ece)


### 👨‍🏫 Teachers

Teachers oversee the students in their courses and can:
- View information about enrolled students
- Access details of the students' parents
  ![image](https://github.com/user-attachments/assets/60019997-c15f-4219-976d-e62b6a4d28d2)
  ![image](https://github.com/user-attachments/assets/5a8a21cb-a1e4-4447-9de4-1b26cfa288c5)

### 👨‍👩‍👧 Parents

Parents manage their children's course enrollments and can:
- Register themselves and create child profiles
  ![image](https://github.com/user-attachments/assets/9cfc123a-b2d1-4a6d-ac09-34dc5ba66a29)
- Browse teacher profiles
  ![image](https://github.com/user-attachments/assets/805dadf0-f797-4092-a4c1-0ec5c60cd975)
- Request course enrollment for their children
  ![image](https://github.com/user-attachments/assets/2d2d4c8e-d3b8-41df-ac1f-513337d2071a)

## 🔄 Workflow

### 1️⃣ Parent Registration & Child Management
- Parents create an account and add their children.

### 2️⃣ Course Enrollment
- Parents browse courses and request to enroll their children.
- Admins approve or reject the enrollment requests.

### 3️⃣ Teacher Management
- Admins create and assign teachers to courses.
- Teachers access their assigned student data.

### 4️⃣ Course Management
- Admins create and configure courses (schedule, pricing, description, etc.), manage enrolled students within their courses.
- Teachers view their courses information, including registered/unregistered children.

## 📚 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.




<h1>Стартиране на приложението</h1>
<ol>
  <li>
    Изтеглете или клонирайте solution-a (кода на приложението);
  </li>
  <li>
    В <i>appsettings.json</i>, който се намира в <i>Scolly/Scolly/appsettings.json</i> , въведете Вашият connection string в кавичките на <i>DefaultConnection</i> (може базата Ви данни да се казва "<i>Scolly</i>"): <strong> "DefaultConnection": "Вашият Connection String"</strong>
  </li>
  <li>
    В <i>Package Manager Console</i>, задайте <i>Default project</i> на <strong><i>Scolly.Infrastructure</i></strong>, напишете командата <strong>Update-Database</strong>, за да се приложат миграциите към базата данни. Приложението има предварително създадени примерни данни в базата данни - Регистрации, Курсове и други.
  </li>
  <li>
    Стартирайте проекта (Build).
  </li>
</ol>
