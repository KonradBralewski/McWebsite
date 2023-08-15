import {IUserPreferencesProvider} from "../../services/UserPreferences/IUserPreferencesProvider"
import { userPreferences } from "../../types/preferences/userPreferences";


export class UserPreferencesProvider implements IUserPreferencesProvider
{
    static injectKey = "UserPreferencesProvider"
    userPreferences : userPreferences;

    constructor(){
        this.userPreferences = {
            darkMode : false
        }

        const darkMode : string | null = localStorage.getItem("darkMode");
        
        if(darkMode === null){
            localStorage.setItem("darkMode", "false");
        }
        else{
            this.userPreferences.darkMode = (/test/).test(darkMode);
        }
    }    
    getPreferences(): userPreferences {
        return this.userPreferences;
    }
    
    setPreferences(updatedPreferences : userPreferences): void {
        Object.entries(this.userPreferences).forEach(entry =>{
            const [key, value] = entry
            localStorage.setItem(key, value.toString())
        })
    }
    
}