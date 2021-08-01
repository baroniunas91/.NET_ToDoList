# .NET Core 3.1 TO-DO list RESTful API
1. Clone repository.
2. Run project (It will automatically create database "todosbyedgaras" in your MySQL server).
3. There will be 3 users in database "admin@test.lt", "user1@test.lt" and "user2@test.lt". Password for all users: "labasrytas123".
4. User with role="admin" can see all users todos and delete all users todos. User with role="user" can create, update and delete his own todos.
5. Don't forget to authenticate and add JWT Bearer token then calling API.
6. If you want to get password reset link to your email please sign up first and then send your email. API endpoint is Login/ForgotPasswordEmail(method:POST).
7. To reset password - API endpoint is Login/ResetPassword(method:POST).
