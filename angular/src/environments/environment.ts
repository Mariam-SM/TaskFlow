import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:61535';

export const environment = {
  production: false,
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
    scope: 'openid profile email offline_access TaskFlow',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'TaskFlow',
    },
  },
} as Environment;
