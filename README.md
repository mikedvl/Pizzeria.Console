# Pizzeria

A console application that processes pizza orders, calculates total prices, and generates ingredient summaries.  
It reads data from files (CSV/JSON), validates the input, and prints a formatted summary to the console.

---

## Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Rider / Visual Studio / VS Code 
- CSV or JSON data files

---

## Configuration

Application settings are managed via `appsettings.json`.  
By default, the application expects the data files to be located in the Data/ directory relative to the executable.
You should configure paths to your data files in the DataFiles section only if you want to override the defaults.

### Sample `appsettings.json`:

```json
{
  "DataFiles": {
    "Orders": "Data/orders.csv",
    "Products": "Data/products.json",
    "Ingredients": "Data/ingredients.json"
  }
}
```

Ensure that the paths are either relative to the working directory or absolute.

---

Running the Application

```dotnet run --project Pizzeria```

Make sure the data files are configured correctly in appsettings.json.

---

Running Tests

Unit and smoke tests are included:

```dotnet test```

Or run tests from your IDE (Rider, Visual Studio, etc.)

---

Example Output
```
===== SUMMARY OVERVIEW =====

Raw order entries (lines in file): 3
Unique order IDs: 2
Total pizza items (by quantity): 6

===== VALID ORDERS SUMMARY =====

Order ID: 1001
CreatedAt: 04.07.2025 10:00:00
will be delivered to 123 Pizza Street
DeliveryAt: 04.07.2025 11:00:00
Items: 3
 - Pizza Margherita x 2
 - Pizza Pepperoni x 1
Total Price: $31.47
Ingredients:
  - Dough: 0,9
  - Tomato Sauce: 0,3
  - Mozzarella: 0,45
  - Pepperoni: 0,2
-------------------------------

Order ID: 1002
CreatedAt: 04.07.2025 09:30:00
will be delivered to 456 Veggie Ave
DeliveryAt: 04.07.2025 10:15:00
Items: 3
 - Pizza Veggie x 3
Total Price: $32.97
Ingredients:
  - Dough: 0,9
  - Tomato Sauce: 0,3
  - Mozzarella: 0,45
  - Vegetables: 0,75
-------------------------------

===== TOTAL INGREDIENTS REQUIRED =====

Dough: 1,8
Tomato Sauce: 0,6
Mozzarella: 0,90
Pepperoni: 0,2
Vegetables: 0,75

Total Price Across All Orders: $64.44

===== END OF SUMMARY =====
```

---

<p align="center">
  <img src="docs/images/pizza2.png" alt="Pizza 1" width="80"/>
  <img src="docs/images/pizza2.png" alt="Pizza 2" width="80"/>
  <img src="docs/images/pizza2.png" alt="Pizza 3" width="80"/>
</p>

