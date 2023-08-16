export type jsonPreferencesObject = {
    darkMode : boolean
}


export class UserPreferences{
    private darkMode : boolean
    constructor() {
        this.darkMode = false
    }

    getDarkMode() : boolean{
        return this.darkMode
    }

    setDarkMode(hasDarkMode : boolean) : void{
        this.darkMode = hasDarkMode;
    }

    getPreferencesAsJson() : jsonPreferencesObject{
        return {
            "darkMode" : this.getDarkMode()
        }
    }
}