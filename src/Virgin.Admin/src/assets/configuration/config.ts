import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';

const b2cPolicies = {
    authorities: {
        signIn: {
            authority: 'your_authority',
        }
    },
    authorityDomain: 'domain'
};

const msalConfig: Configuration = {
    auth: {
        clientId: 'clientID',
        authority: b2cPolicies.authorities.signIn.authority,
        knownAuthorities: [b2cPolicies.authorityDomain],
        redirectUri: 'uri',
        postLogoutRedirectUri: 'uri',
        navigateToLoginRequestUrl: true,
    },
    cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage,
    }
};

const protectedResources = {
    todoListApi: {
        endpoint: 'api_endpoint',
        scopes: ['your_scope'],
    },
}

export const appConfig = {
    apiUrl: 'api_url',
    msalConfig,
    protectedResources
};
