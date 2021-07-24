﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface ITodoRepository
    {
        List<TodosGetDto> Get();
        void AddTodo(TodoDto todoDto, string loggedUserEmail);
        void UpdateTodo(TodoDto todoDto);
        void DeleteTodo(int id);
    }
}
