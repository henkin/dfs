using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using todos.Models;
using Todos.Models;

namespace todos.Web.ApiModels
{
    public class TaskListModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskModel> Tasks { get; set; }

        public TaskListModel()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, {Description}, \n {string.Join("\n ", Tasks)}";
        }

        public override bool Equals(object obj)
        {
            return PropertiesEqual(obj);
        }

        public bool PropertiesEqual(object comparisonObject)
        {

            Type sourceType = this.GetType();
            Type destinationType = comparisonObject.GetType();

            if (sourceType == destinationType)
            {
                PropertyInfo[] sourceProperties = sourceType.GetProperties();
                foreach (PropertyInfo pi in sourceProperties)
                {
                    if ((sourceType.GetProperty(pi.Name).GetValue(this, null) == null && destinationType.GetProperty(pi.Name).GetValue(comparisonObject, null) == null))
                    {
                        // if both are null, don't try to compare  (throws exception)
                    }
                    else if (sourceType.GetProperty(pi.Name).GetValue(this, null).ToString() != destinationType.GetProperty(pi.Name).GetValue(comparisonObject, null).ToString())
                    {
                        // only need one property to be different to fail Equals.
                        return false;
                    }
                }
            }
            else
            {
                return base.Equals(comparisonObject);
            }

            return true;
        }
    }

}