# Request

A local travel agent has commissioned you as a Junior Software Developer to write a program. The program will be used by staff to calculate the costs of converting pounds
sterling (GBP) into another currency.

The travel agent only converts GBP into:

- American Dollars (USD)
- Euros (EUR)
- Brazilian Real (BRL)
- Japanese Yen (JPY)
- Turkish Lira (TRY)

# Objective

You need to create a program that:

- allows the travel agent to enter the amount in pounds sterling (GBP) that the customer wishes to convert
- allows the travel agent to choose the currency requested by the customer
- converts the amount entered in GBP to the chosen currency
- calculates a transaction fee depending on how much in GBP the customer converts
- calculates the total cost (amount to convert plus transaction fee) in GBP of converting to the chosen currency
- applies a discount of 5% to the total cost if the customer is also a member of staff

# Success Criteria

You must design, implement, and test your program. You must also justify and evaluate your decisions.

When you are designing and developing the solution ensure that:

- standard programming conventions have been followed
- it is efficient and robust
- it is user friendly
- it provides accurate currency conversions
- all currency values are displayed to two decimal places
- there is output to show:
    - how much chosen currency the customer receives
    - the transaction fee
    - the discount amount
    - the total cost

# Information

## Exchange Rates

| Currency         | Exchange rate (1 GBP =) |
|------------------|-------------------------|
| American Dollars | 1.40 USD                |
| Euros            | 1.14 EUR                |
| Brazilian Real   | 4.77 BRL                |
| Japanese Yen     | 151.05 JPY              |
| Turkish Lira     | 5.68 TRY                |

## Conversion Limit

Maximum of 2500 GBP can be converted in one transaction.

## Transaction Fees

| Amount        | Fee  |
|---------------|------|
| Up to 300 GBP | 3.5% |
| Over 300 GBP  | 3%   |
| Over 750 GBP  | 2.5% |
| Over 1000 GBP | 2%   |
| Over 2000 GBP | 1.5% |
