type category = {
    id : number,
    name : string,
    docsUri : string | null,
    level : number,
    subCategories : category[] | null
}

export default category;