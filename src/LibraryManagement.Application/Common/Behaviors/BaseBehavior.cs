using ErrorOr;
using FluentValidation;
using MediatR;

namespace LibraryManagement.Application.Common.Behaviors;

public class BaseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
where TResponse : IErrorOr
{
    private readonly IValidator<TRequest> _validator;

    public BaseBehavior(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator != null)
            return await next();

        var validateResult = _validator.Validate(request);

        if (validateResult.IsValid)
            return await next();

        var result = validateResult.Errors.Select(x => Error.Validation(x.ErrorMessage)).ToList();

        return (dynamic)result;
    }
}