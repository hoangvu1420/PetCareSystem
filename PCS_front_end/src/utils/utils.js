function convertDate(d) {
    // This function convert a date string to regular date string
    // Example: a JSON date string -> 24-05-2024
    return (new Date(d)).toLocaleDateString('en-GB')
}

function formatPrice(price) {
    // This function format price by adding a comma every 3 digits
    // Example: 56000 -> 56,000
    const priceString = price.toString();

    const formattedPrice = priceString.replace(/\B(?=(\d{3})+(?!\d))/g, ",");

    return formattedPrice;
}

export default { convertDate, formatPrice }