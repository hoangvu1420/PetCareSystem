function convertDate(d) {
    return (new Date(d)).toLocaleDateString('en-GB')
}

function formatPrice(price) {
    // Convert the price to a string
    const priceString = price.toString();

    // Use a regular expression to add commas
    const formattedPrice = priceString.replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    return formattedPrice;
}

export default { convertDate, formatPrice }