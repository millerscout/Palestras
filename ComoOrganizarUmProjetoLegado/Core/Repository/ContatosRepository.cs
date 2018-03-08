using Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
	public interface IContatosRepository : IBaseRepository<Contato> { }

	public class ContatosRepository : BaseRepository<CoreEntities, Contato>, IContatosRepository
	{
		public ContatosRepository()
			: base(new ContatoValidator()) { }

		public ContatosRepository(CoreEntities context)
			: base(context, new ContatoValidator()) { }
	}
	public class ContatoValidator : AbstractValidator<Contato>
	{

		public ContatoValidator()
		{
			RuleFor(c => c.Nome).Must(c => !c.Contains("ilegal")).WithMessage("Nome ilegal");
		}
	}
}
