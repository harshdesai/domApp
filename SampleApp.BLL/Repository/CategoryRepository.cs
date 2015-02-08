using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.BLL.Interface;
using SampleApp.Entity;
namespace SampleApp.BLL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        SampleAppEntities _entity = null;

        public CategoryRepository()
        {
            _entity = new SampleAppEntities();
        }

        public List<Catagory> GetCategory()
        {
            List<Catagory> list = _entity.Catagories.ToList();
            return list;
        }

    }
}
