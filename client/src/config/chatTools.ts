/**
 * Defines the schemas for the client-side functions exposed to the local Ollama LLM as tools.
 */
export const CHAT_TOOLS = [
  {
    type: 'function',
    function: {
      name: 'get_tasks',
      description: 'Retrieves the list of onboarding tasks for a specific department role (e.g. engineering, sales).',
      parameters: {
        type: 'object',
        properties: {
          role: {
            type: 'string',
            description: "The department role key (e.g. 'engineering', 'sales', 'marketing', 'hr', 'finance')"
          }
        },
        required: ['role']
      }
    }
  },
  {
    type: 'function',
    function: {
      name: 'get_dashboard',
      description: 'Retrieves department details (office/remote schedule and team contacts like onboarding buddy or manager) for a specific department role key.',
      parameters: {
        type: 'object',
        properties: {
          role: {
            type: 'string',
            description: "The department role key (e.g. 'engineering', 'sales', 'marketing', 'hr', 'finance')"
          }
        },
        required: ['role']
      }
    }
  },
  {
    type: 'function',
    function: {
      name: 'get_task_instructions',
      description: 'Retrieves step-by-step detailed instructions for a specific task using its numeric task ID.',
      parameters: {
        type: 'object',
        properties: {
          taskId: {
            type: 'integer',
            description: 'The numeric ID of the onboarding task.'
          }
        },
        required: ['taskId']
      }
    }
  }
]
