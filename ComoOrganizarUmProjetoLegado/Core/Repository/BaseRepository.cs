using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data.Entity.Validation;

namespace Core.Repository
{
	public abstract class BaseRepository<C, T> :
	IBaseRepository<T> where T : class where C : Models.CoreEntities, new()
	{
		private C _entities;
		private AbstractValidator<T> _validator;

		public BaseRepository(AbstractValidator<T> validator)
		{
			_entities = new C();
			_validator = validator;
		}

		public BaseRepository(C context, AbstractValidator<T> validator)
		{
			_entities = context;
			_validator = validator;
		}


		public C Context
		{

			get { return _entities; }
			set { _entities = value; }
		}

		public virtual IQueryable<T> GetAll()
		{

			IQueryable<T> query = _entities.Set<T>();
			return query;
		}

		public T FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return _entities.Set<T>().FirstOrDefault(predicate);
		}

		public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
		{

			IQueryable<T> query = _entities.Set<T>().Where(predicate);
			return query;
		}

		public virtual void Add(ICollection<T> entities)
		{

			List<ValidationResult> results = new List<ValidationResult>();
			foreach (var entity in entities)
			{
				var result = _validator.Validate(entity);
				if (!result.IsValid)
				{
					results.Add(result);
				}
			}

			if (results.Count() > 0)
			{
				throw new CoreValidationException(results);
			}

			_entities.Set<T>().AddRange(entities);
		}

		public virtual void Add(T entity)
		{
			var state = _validator.Validate(entity);

			if (!state.IsValid)
			{
				throw new CoreValidationException(state);

			}
			_entities.Set<T>().Add(entity);
		}

		public virtual void Delete(T entity)
		{
			_entities.Set<T>().Remove(entity);
		}

		public virtual void Update(T entity)
		{
			_validator.Validate(entity);
			_entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
		}
		public void Update(ICollection<T> entities)
		{
			List<ValidationResult> results = new List<ValidationResult>();
			foreach (var entity in entities)
			{
				var result = _validator.Validate(entity);
				if (!result.IsValid)
				{
					results.Add(result);
				}
			}

			if (results.Count() > 0)
			{
				throw new CoreValidationException(results);
			}

			_entities.Set<T>().AddRange(entities);

			foreach (var entity in entities)
			{
				_entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
			}
		}

		public virtual void Save()
		{
			try
			{
				_entities.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				var errorMessages = ex.EntityValidationErrors
						.SelectMany(x => x.ValidationErrors)
						.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage));

				throw new CoreValidationException(new ValidationResult(errorMessages));
			}
		}

	}

	[Serializable]
	public class CoreValidationException : Exception
	{
		public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

		public CoreValidationException(ValidationResult result)
		{
			AddErrorsToList(result);
		}

		private void AddErrorsToList(ValidationResult result)
		{
			foreach (var item in result.Errors)
			{
				var key = item.PropertyName;
				if (Errors.ContainsKey(key))
				{
					Errors[key].Add(item.ErrorMessage);
				}
				else
				{
					Errors.Add(key, new List<string> { item.ErrorMessage });
				}
			}
		}

		public CoreValidationException(List<ValidationResult> results)
		{
			results.ForEach((error) =>
			{
				AddErrorsToList(error);
			});
		}
	}

	public interface IBaseRepository<T> where T : class
	{

		IQueryable<T> GetAll();
		IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
		T FirstOrDefault(Expression<Func<T, bool>> predicate);
		void Add(T entity);
		void Add(ICollection<T> entities);
		void Delete(T entity);
		void Update(T entity);
		void Update(ICollection<T> entity);
		void Save();
	}
}
