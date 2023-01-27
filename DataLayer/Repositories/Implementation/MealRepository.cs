using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Implementation
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository
    {
        public MealRepository(FoodContext context) : base(context)
        {
        }
    }
}
