import {userPreferences} from "../../types/preferences/userPreferences"

export interface IUserPreferencesProvider{
    getPreferences() : userPreferences
    setPreferences(updatedPreferences : userPreferences) : void
}