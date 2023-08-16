import { UserPreferencesProvider } from "#root/src/common/services/UserPreferences/UserPreferencesProvider";

const services = {
  [UserPreferencesProvider.injectKey]: new UserPreferencesProvider(),
};

export function provideServices(appInstance: any) {
  Object.entries(services).forEach((serviceEntry) => {
    const [key, value] = serviceEntry;
    appInstance.provide(key, value);
  });
}
