import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4300';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TaskFlow',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44332/',
    redirectUri: baseUrl,
    clientId: 'TaskFlow_App',
    responseType: 'code',
    scope: 'offline_access TaskFlow',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'TaskFlow',
    },
  },
} as Environment;
