﻿namespace ToDoList.RestfulAPI.Dto
{
    public class TodosGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}
