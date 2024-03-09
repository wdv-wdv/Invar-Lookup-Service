<!--TASK_INSTRUCTIONS_START-->
# Lookup Service - Level 1

Your task is to build a backend service that implements the [Lookup Service REST API](https://infra.devskills.app/lookup/api/1.0.0) and integrates with the [Credit Data REST API](https://infra.devskills.app/credit-data/api/1.0.0) to aggregate historical credit data.

<details>
<summary>Lookup Service REST API Specification</summary>

```json
{
  "openapi": "3.0.0",
  "info": {
    "title": "Lookup Service API",
    "version": "1.0.0"
  },
  "paths": {
    "/ping": {
      "get": {
        "summary": "Healhcheck to make sure the service is up.",
        "responses": {
          "200": {
            "description": "The service is up and running."
          }
        }
      }
    },
    "/credit-data/{ssn}": {
      "get": {
        "summary": "Return aggregated credit data.",
        "parameters": [
          {
            "name": "ssn",
            "in": "path",
            "required": true,
            "description": "Social security number.",
            "schema": {
              "type": "string"
            },
            "example": "424-11-9327"
          }
        ],
        "responses": {
          "200": {
            "description": "Aggregated credit data for given ssn.",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/CreditData"
                },
                "examples": {
                  "CreditDataEmma": {
                    "$ref": "#/components/examples/CreditDataEmma"
                  }
                }
              }
            }
          },
          "404": {
            "description": "Credit data not found for given ssn."
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "CreditData": {
        "type": "object",
        "properties": {
          "first_name": {
            "type": "string"
          },
          "last_name": {
            "type": "string"
          },
          "address": {
            "type": "string"
          },
          "assessed_income": {
            "type": "integer"
          },
          "balance_of_debt": {
            "type": "integer"
          },
          "complaints": {
            "type": "boolean"
          }
        }
      }
    },
    "examples": {
      "CreditDataEmma": {
        "value": {
          "first_name": "Emma",
          "last_name": "Gautrey",
          "address": "09 Westend Terrace",
          "assessed_income": 60668,
          "balance_of_debt": 11585,
          "complaints": true
        }
      }
    }
  }
}
```
</details>

<details>
<summary>Credit Data REST API Specification</summary>

```json
{
  "openapi": "3.0.0",
  "info": {
    "title": "Credit Data API",
    "version": "1.0.0"
  },
  "paths": {
    "/personal-details/{ssn}": {
      "get": {
        "summary": "Return personal details.",
        "parameters": [
          {
            "name": "ssn",
            "in": "path",
            "required": true,
            "description": "Social security number.",
            "schema": {
              "type": "string"
            },
            "example": "424-11-9327"
          }
        ],
        "responses": {
          "200": {
            "description": "Personal details for given ssn.",
            "headers": {
              "Cache-Control": {
                "schema": {
                  "type": "string"
                },
                "description": "Cache-Control response directives, e.g. \"private, max-age=604800\" or \"no-store\""
              }
            },
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/PersonalDetails"
                },
                "examples": {
                  "PersonalDetailsEmma": {
                    "$ref": "#/components/examples/PersonalDetailsEmma"
                  }
                }
              }
            }
          },
          "404": {
            "description": "Personal data not found for given ssn."
          }
        }
      }
    },
    "/assessed-income/{ssn}": {
      "get": {
        "summary": "Return assessed details income.",
        "parameters": [
          {
            "name": "ssn",
            "in": "path",
            "required": true,
            "description": "Social security number.",
            "schema": {
              "type": "string"
            },
            "example": "424-11-9327"
          }
        ],
        "responses": {
          "200": {
            "description": "Assessed income details for given ssn.",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/AssessedIncomeDetails"
                },
                "examples": {
                  "AssessedIncomeEmma": {
                    "$ref": "#/components/examples/AssessedIncomeDetailsEmma"
                  }
                }
              }
            }
          },
          "404": {
            "description": "Personal data not found for given ssn."
          }
        }
      }
    },
    "/debt/{ssn}": {
      "get": {
        "summary": "Return debt details.",
        "parameters": [
          {
            "name": "ssn",
            "in": "path",
            "required": true,
            "description": "Social security number.",
            "schema": {
              "type": "string"
            },
            "example": "424-11-9327"
          }
        ],
        "responses": {
          "200": {
            "description": "Debt details for given ssn.",
            "content": {
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/DebtDetails"
                },
                "examples": {
                  "DebtEmma": {
                    "$ref": "#/components/examples/DebtDetailsEmma"
                  }
                }
              }
            }
          },
          "404": {
            "description": "Personal data not found for given ssn."
          }
        }
      }
    }
  },
  "servers": [
    {
      "url": "https://infra.devskills.app/api/credit-data"
    }
  ],
  "components": {
    "schemas": {
      "PersonalDetails": {
        "type": "object",
        "properties": {
          "first_name": {
            "type": "string"
          },
          "last_name": {
            "type": "string"
          },
          "address": {
            "type": "string"
          }
        }
      },
      "AssessedIncomeDetails": {
        "type": "object",
        "properties": {
          "assessed_income": {
            "type": "integer"
          }
        }
      },
      "DebtDetails": {
        "type": "object",
        "properties": {
          "balance_of_debt": {
            "type": "integer"
          },
          "complaints": {
            "type": "boolean"
          }
        }
      }
    },
    "examples": {
      "PersonalDetailsEmma": {
        "value": {
          "first_name": "Emma",
          "last_name": "Gautrey",
          "address": "09 Westend Terrace"
        }
      },
      "AssessedIncomeDetailsEmma": {
        "value": {
          "assessed_income": 60668
        }
      },
      "DebtDetailsEmma": {
        "value": {
          "balance_of_debt": 11585,
          "complaints": true
        }
      }
    }
  }
}
```
</details>

Please agree with the hiring team regarding the tech stack choice.

## Additional requirements

- Do your best to make the [provided E2E tests](cypress/e2e/test.cy.js) pass.

<!--TASK_INSTRUCTIONS_END-->

## Getting started

<details>
  <summary>If you run into a problem</summary>
  
1. **Open a [GitHub Issue](https:\/\/docs.github.com\/en\/issues\/tracking-your-work-with-issues\/creating-an-issue):** Simply go to the "Issues" tab in this repository and click on "New issue".
2. **Describe Your Issue:** Briefly describe the problem you are encountering. Include key details like error messages or steps to reproduce the issue. This helps us understand and resolve your concern more efficiently.
3. **Automated Support:** Initially, our support bot will try to resolve your issue. If it is unable to help, a member of the Alva team will be notified and will step in to assist you.

**Note:** it is important to close the issue once your problem is resolved, open issues may indicate to the hiring team that your assignment is not yet ready for review.

</details>

<details>
  <summary>Import a starter project</summary>

  We have created a set of starter projects with different tech stacks to help you get started quickly.

  To import a starter project:
  
  1. Go to the "Actions" tab of your GitHub repository and select the "Setup boilerplate" workflow in the left side panel.
  2. In the "Run workflow" dropdown, select the desired boilerplate along with the branch name where you want the boilerplate to be imported (e.g., `implementation`) and click the "Run workflow" button (you can find all starter projects' definitions [here](https://help.alvalabs.io/en/articles/7972852-supported-coding-test-boilerplates)).
  
  After the workflow has finished, your selected boilerplate will be imported to the specified branch, and you can continue from there.
  
  > ⚠️ **Custom setup**
  > 
  > If you instead want to set up a custom project, complete the steps below to make the E2E tests run correctly:
  > 1. Update the `baseUrl` (where your frontend runs) in [cypress.config.js](cypress.config.js).
  > 2. Update the [`build`](package.json#L5) and [`start`](package.json#L6) scripts in [package.json](package.json) to respectively build and start your app.
  
</details>

<details>
  <summary>Prepare for coding</summary>

  To get this repository to your local machine, clone it with `git clone`.

  Alternatively, spin up a pre-configured in-browser IDE by clicking on the "Code" tab in this repository and then "Create codespace on {branch_name}".
  
  ![CleanShot 2023-10-13 at 00 00 32@2x](https://github.com/DevSkillsHQ/transaction-management-fullstack-level-1/assets/1162212/598ff1ae-238d-4691-8b7c-eb2228fdefac)

</details>

<details>
  <summary>Running the E2E tests</summary>

  > ⚠️ Before executing the tests, ensure [Node](https://nodejs.org/en) is installed and your app is running.

  ```bash
  npm install
  npm run test
  ```

</details>

## Submitting your solution for review

1. Create a new `implementation` branch on this repository and push your code there.
2. Create a new pull request from `implementation` **without merging it**.
5. Document the tech decisions you've made by creating a new review on your PR ([see how](https://www.loom.com/share/94ae305e7fbf45d592099ac9f40d4274)).
6. Await further instructions from the hiring team.

## Time estimate

Between 30 minutes - 1 hour + the time to set up the project/environment (we suggest importing one of the provided project starters to save time).

However, there is no countdown. The estimate is for you to plan your time.

---

Authored by [Alva Labs](https://www.alvalabs.io/).
