import category from "./category";

type clickableCategory = category & {
    'wasClicked' : boolean,
    subCategories : clickableCategory[]
}

export default clickableCategory;