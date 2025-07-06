using Pizzeria.Models.Dto;

namespace Pizzeria.Services.Interfaces;

public interface ISummaryPrinter
{ 
    void Print(SummaryResult summary);
}