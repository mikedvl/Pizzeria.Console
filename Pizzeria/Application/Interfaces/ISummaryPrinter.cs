using Pizzeria.Domain.DTO;

namespace Pizzeria.Application.Interfaces;

public interface ISummaryPrinter
{ 
    void Print(SummaryResult summary);
}