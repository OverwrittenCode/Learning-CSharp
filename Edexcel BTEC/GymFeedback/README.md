# Request

A local gym has commissioned you as a software developer to write a program that will assess a gym member’s requirement to maintain their current weight accurately.

# Objective

You need to create a program that will give a gym member information about:

- their current basal metabolic rate (BMR)
- their current body mass index (BMI)
- their target BMI
- the number of kilocalories to maintain their current weight

# Success Criteria

You will design, implement, and test your program. You will also need to justify and evaluate your decisions.

When designing and developing the solution ensure:

- it is efficient and robust
- it provides the accurate daily intake requirement for a gym member to maintain their body mass index (BMI)
- the BMR calculation is given to 2 decimal places
- the BMI calculation is given to 1 decimal place
- the kilocalorie requirement output is shown rounded to a whole number
- there is a text output to show the member’s:
    - current BMR
    - current BMI
    - target BMI

# Information

You are provided with information to use when designing and developing your program:

- the revised Harris-Benedict equation used to calculate Basal Metabolic Rate
- the formula to use to give the recommended daily kilocalorie intake to maintain current weight for men and women
- the formula to calculate BMI
- the standard BMI categories
- current gym membership age, weight, and height profile

## Calculating the Basal Metabolic Rate (BMR) using the revised Harris-Benedict equation:

**Men:**
`BMR = 88.362 + (13.397 × weight in kg) + (4.799 × height in cm) – (5.677 × age in years)`

**Women:**
`BMR = 447.593 + (9.247 × weight in kg) + (3.098 × height in cm) – (4.330 × age in years)`

## Calculating the recommended daily kilocalorie intake to maintain current weight:

| Individual’s level of exercise                            | Calculation of daily intake required (kilocalories) |
|-----------------------------------------------------------|-----------------------------------------------------|
| Little to no exercise                                     | BMR × 1.2                                           |
| Light exercise (1–3 days per week)                        | BMR × 1.375                                         |
| Moderate exercise (3–5 days per week)                     | BMR × 1.55                                          |
| Heavy exercise (6–7 days per week)                        | BMR × 1.725                                         |
| Very heavy exercise (twice per day, extra heavy workouts) | BMR × 1.9                                           |

## Calculating BMI

`BMI = Weight (kg) / (Height (m) × Height (m))`

## Standard BMI categories

- Underweight: < 18.5
- Normal weight: 18.5–24.9
- Overweight: 25–29.9
- Obesity: BMI of 30 or greater

**Ideal gym member BMI:** 22

**Current gym membership age, weight, and height profile:**

- Membership age range: 14–100 years of age
- Weight range: 30–250 kg
- Height range: 120–210 cm
