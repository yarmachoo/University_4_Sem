using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Application.AuthorUseCases.Commands;
public class UpdateAuthorHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateAuthorCommand>
{
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AuthorRepository.UpdateAsync(request.Author, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
