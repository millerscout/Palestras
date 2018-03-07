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
			: base(new AlunoValidator()) { }

		public ContatosRepository(CoreEntities context)
			: base(context, new AlunoValidator()) { }
	}
	public class AlunoValidator : AbstractValidator<Contato> { }
}
