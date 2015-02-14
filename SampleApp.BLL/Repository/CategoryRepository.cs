using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.BLL.Interface;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Repository
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
