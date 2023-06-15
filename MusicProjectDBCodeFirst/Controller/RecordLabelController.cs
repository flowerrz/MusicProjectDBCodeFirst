using System;
using System.Collections.Generic;
using System.Text;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class RecordLabelController
    {
        MusicProjectDBContext context;

        public RecordLabelController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddRecordLabel(string labelName, string country)
        {
            context.RecordLabels.Add(new RecordLabel
            {
                LabelName = labelName,
                CountryName = country
            });
            context.SaveChanges();
        }

        public RecordLabel GetLabelByName(string labelName)
        {
            return context.RecordLabels.FirstOrDefault(l => l.LabelName == labelName);
        }

        public void DeleteRecordLabel(string name)
        {
            context.RecordLabels.Remove(context.RecordLabels.FirstOrDefault(y => y.LabelName == name));
            context.SaveChanges();
        }

        public virtual ICollection<RecordLabel> GetAll()
        {
            return context.RecordLabels.ToList();
        }
    }
}
