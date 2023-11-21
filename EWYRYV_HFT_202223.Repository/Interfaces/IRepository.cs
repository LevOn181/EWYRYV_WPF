﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWYRYV_HFT_202223.Repository
{
	public interface IRepository<T> where T : class
	{ 
		IQueryable<T> ReadAll();
		T Read(int id);
		void Create(T item);
		void Update(T item);
		void Delete(int id);
	}
}