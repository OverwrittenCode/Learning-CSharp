# Request

A data storage company has commissioned you as a Junior Software Developer to write a
program to help its staff create secure passwords.

The program will use a scoring system
to provide a security rating for the password.

# Objective

You need to create a program that will allow staff to enter this information:

- a password to be rated.
The program should then provide the user with details to show:
- whether or not the password is too short or too long
- the breakdown of the points scored for the password
- the total points score for the password
- a security rating for the password based on the total score.

# Success Crtieria
You must design, implement and test your program. You must also justify and evaluate
your decisions.

When you are designing and developing the solution ensure that:
- standard programming conventions have been followed
- it is efficient and robust
- it is user friendly
- the program is tested using normal, abnormal and extreme data
- there is output to show the scoring and rating of the password.

# Information
You are provided with this information to use when designing and developing
your program:

- minimum and maximum length of password
- characters allowed in password
- password scoring system
- password security ratings.

## Minimum and maximum length of password
- Minimum length = 8 characters
- Maximum length = 15 characters

## Characters allowed in a password
- Special characters chosen from: `! % & * + =`
- Lower case letters
- Upper case letters
- Numeric characters

## Password scoring system

### Positive scores
| Item                                        | Points added               |
|---------------------------------------------|----------------------------|
| Lower case letter                           | 5 per letter               |
| Upper case letter                           | 5 per letter               |
| Numeric characters                          | 10 per numeric character   |
| Special characters                          | 10 per special character   |
| Password contains 10 or more characters     | 20                         |

### Negative scores
| Item                                        | Points deducted            |
|---------------------------------------------|----------------------------|
| Lower case letters only                     | 3 per character            |
| Upper case letters only                     | 3 per character            |
| Numeric characters only                     | 5 per character            |

## Password security ratings:

| Rating        | Score range      |
|---------------|------------------|
| Very low      | 20 or less       |
| Low           | 21 to 40         |
| Medium        | 41 to 70         |
| High          | 71 to 80         |
| Very high     | 81 and above     |


