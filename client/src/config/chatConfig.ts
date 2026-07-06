/**
 * Central configuration file for the Ask Meridian onboarding assistant.
 * Exposes system prompts, Ollama endpoints, default messages, and UI text elements.
 */
export const CHAT_CONFIG = {
  // Local Ollama endpoints
  OLLAMA_BASE_URL: 'http://localhost:11434',
  OLLAMA_CHAT_ENDPOINT: 'http://localhost:11434/api/chat',

  // Target model
  MODEL_NAME: 'gemma4:latest',

  // Customizable initial system prompt injected into LLM queries
  SYSTEM_PROMPT: 
    'You are the Meridian Navigator onboarding assistant. Help the new hire navigate their department onboarding tracks. ' +
    'Provide concise, friendly, and structured advice. Do not include markdown or emoticons in your responses. Use the get_tasks tool to fetch their onboarding checklist when they ask about their tasks.',

  // Dynamic greetings and introductions
  DEFAULT_WELCOME: 
    "Hi! Welcome to Meridian. I'm your onboarding assistant. Select a department role or ask me general onboarding questions to get started.",
  
  ROLE_WELCOME_TEMPLATE: (deptName: string) => 
    `Welcome to the ${deptName} team! I'm your Meridian Navigator assistant. Ask me anything about your department tasks, development requirements, or tool setups.`,

  // UI labels and titles
  DRAWER_TITLE: 'Ask Meridian',
  BOT_NAME: 'Navigator Bot',
  STATUS_ACTIVE: 'Active',
  STATUS_INACTIVE: 'Inactive',
  INPUT_PLACEHOLDER: 'Ask about setups, buddies, etc...',
  SUGGESTED_HEADER: 'Suggested Questions',

  // Fallback local response text when Ollama is offline or model is missing
  FALLBACK_RESPONSE: 
    "I'm not sure I have details on that specific question yet. Try asking about 'setup instructions', 'docker command', 'who is my buddy', or reach out to your department manager listed on the team contacts dashboard."
}
