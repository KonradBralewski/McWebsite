import {IUserPreferencesProvider} from "../../services/UserPreferences/IUserPreferencesProvider"
import { UserPreferences } from "../../types/preferences/UserPreferences";

export class UserPreferencesProvider implements IUserPreferencesProvider
{
    static injectKey = "UserPreferencesProvider"
    private userPreferences : UserPreferences;

    constructor(){
        this.userPreferences = new UserPreferences()

        const darkMode : string | null = localStorage.getItem("darkMode");

        if(darkMode === null){
            localStorage.setItem("darkMode", "false");
        }
        else{
            this.userPreferences.setDarkMode((/true/).test(darkMode));
        }
    }
    
    getPreferences(): UserPreferences {
        return this.userPreferences;
    }
    
    setPreferences(updatedPreferences : UserPreferences): void {
        Object.entries(updatedPreferences.getPreferencesAsJson()).forEach(entry =>{
            const [key, value] = entry

            localStorage.setItem(key, value.toString())
        })
        
        this.userPreferences = updatedPreferences
    }
    
}