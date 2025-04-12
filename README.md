# 🧠 Bitasmbl Backend

Welcome to the **backend of Bitasmbl** — a collaboration platform designed to connect skilled developers and creators, enabling them to form teams, build real-world projects, and validate ideas through hands-on experience.

---

## 🚀 What is Bitasmbl?

Bitasmbl helps:

- 🌱 Aspiring developers gain real-world project experience  
- 💡 Innovators find collaborators with the right skillsets  
- 🧩 Teams build MVPs and validate product-market fit  

Unlike traditional job boards or freelance platforms, Bitasmbl emphasizes **collaboration over competition**, making it the perfect place for juniors, mid-levels, and creators to build together.

---

## 🏗️ Backend Stack

- **.NET Core 8** — Web API  
- **Entity Framework Core** — ORM for MSSQL  
- **SignalR** — Real-time communication (project notifications)  
- **Amazon S3** — File storage for project assets and user avatars  
- **Cookie Authentication** — Session-based auth  
- **Google & GitHub OAuth** — Secure third-party login  
- **MSSQL** — Primary relational database  

---

## ✨ Core Features

- 🔐 User authentication with GitHub & Google  
- 🧠 Project creation with category & tech requirements  
- 📩 Application + approval system for project collaboration  
- 🔔 SignalR-powered real-time notifications  
- ☁️ AWS S3 integration for file uploads  
- 🔍 Skill-based matching logic (in progress)

---

## 🧪 How to Run Locally

```bash
# 1. Clone the repo
git clone https://github.com/he1snber8/bitasmbl_BE.git

# 2. Navigate into the project
cd bitasmbl-BE

# 3. Setup your database and secrets (appsettings.json)

# 4. Apply database migrations
dotnet ef database update

# 5. Start the backend server
dotnet run
```

---

## 🔒 Environment & Secrets

Ensure the following are set in your environment:

- AWS Access Key & Secret  
- GitHub/Google OAuth credentials  
- MSSQL connection string  

---

## 📬 Contact & Collaboration

Interested in contributing or collaborating?

- 📧 Email: [lukakhaja@yahoo.com](mailto:lukakhaja@yahoo.com)  
- 🌐 Platform: [bitasmbl.com](https://bitasmbl.com)
