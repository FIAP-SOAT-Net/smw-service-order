using Microsoft.AspNetCore.Mvc;
using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.Adapters.Presenters;

public static class ActionResultPresenter
{
    public static ActionResult ToActionResult(Response result) => new ObjectResult(result) { StatusCode = (int?) result.StatusCode };

    public static ActionResult ToActionResult<T>(Response<T> result) => new ObjectResult(result) { StatusCode = (int?) result.StatusCode };
}
