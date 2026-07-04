export interface DepartmentConfig {
  id: number;
  name: string;
  roleKey: string;
  tagline: string;
  welcomeMessage: string;
}

export const DEPARTMENTS: DepartmentConfig[] = [
  {
    id: 1,
    name: 'Engineering',
    roleKey: 'engineering',
    tagline: 'Code, build, and deploy the next generation of Meridian solutions.',
    welcomeMessage: 'Welcome to the Meridian Engineering team! Your onboarding checklist focuses on setting up your local dev environment, cloning our repositories, and shipping your first pull request.'
  },
  {
    id: 2,
    name: 'Sales',
    roleKey: 'sales',
    tagline: 'Connect clients with value and expand the Meridian ecosystem.',
    welcomeMessage: 'Welcome to the Meridian Sales team! Your onboarding journey covers CRM setups, shadowing customer calls, and mastering our product pitch.'
  },
  {
    id: 3,
    name: 'Marketing',
    roleKey: 'marketing',
    tagline: 'Define the brand voice and reach audiences worldwide.',
    welcomeMessage: 'Welcome to the Meridian Marketing team! You will dive into current active campaigns, brand calendars, and content guidelines.'
  },
  {
    id: 4,
    name: 'HR',
    roleKey: 'hr',
    tagline: 'Empower our people, champion culture, and streamline support.',
    welcomeMessage: 'Welcome to the Meridian HR team! We will guide you through internal tools, company handbook compliance, and benefit enrollment.'
  },
  {
    id: 5,
    name: 'Finance',
    roleKey: 'finance',
    tagline: 'Drive fiscal health and budget strategy across the organization.',
    welcomeMessage: 'Welcome to the Meridian Finance team! Your path details expense policies, budget templates, and monthly close workflows.'
  }
];
