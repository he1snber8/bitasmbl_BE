# 🧠 Bitasmbl Backend

# Welcome to the backend of Bitasmbl — a collaboration platform built to connect skilled developers and creators, enabling them to form teams, 
build real-world projects,and validate product ideas through hands-on experience.

# 🚀 What is Bitasmbl?

Bitasmbl is a platform that helps:

    Aspiring developers gain real-world project experience

    Innovators find collaborators with the right skillsets

    Teams build MVPs together and prove the value of their ideas

Unlike traditional job boards or freelance sites, Bitasmbl focuses on collaboration over competition — fostering a space for juniors, mid-levels, 
and passionate creators to turn ideas into tangible products.

# 🏗️ Backend Stack

    .NET Core 8 — Web API

    Entity Framework Core — ORM for MSSQL

    SignalR — Real-time communication (e.g., project notifications)

    Amazon S3 — File storage (project assets, avatars)

    Cookie Auth — Auth flow for external and internal users

    Google & GitHub OAuth — Third-party authentication

    MSSQL — Primary database

# ✨ Core Features

    🔐 User authentication with GitHub/Google

    🧑‍💻 Project creation with categories and tech requirements

    📨 Application & approval system between devs and project owners

    💬 SignalR-powered real-time notification system

    📦 AWS S3 integration for media uploads

    🔎 Skill & role matching logic (WIP)

# 🧪 How to Run Locally

 1. Clone the repo
git clone https://github.com/your-username/bitasmbl-backend.git

 2. Setup database and secrets (appsettings.json)

 3. Run migrations
dotnet ef database update

 4. Start the backend
dotnet run

🔒 Environment & Secrets

Be sure to configure:

    AWS credentials

    GitHub/Google OAuth Client IDs

    Connection strings for MSSQL

📬 Contact & Collaboration

Interested in contributing?
Want to join the project or ask questions?

Email: lukakhaja@yahoo.com
Platform: bitasmbl.com
