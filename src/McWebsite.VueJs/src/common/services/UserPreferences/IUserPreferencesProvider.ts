import { UserPreferences } from "../../types/preferences/UserPreferences";

export interface IUserPreferencesProvider {
  getPreferences(): UserPreferences;
  setPreferences(updatedPreferences: UserPreferences): void;
}
