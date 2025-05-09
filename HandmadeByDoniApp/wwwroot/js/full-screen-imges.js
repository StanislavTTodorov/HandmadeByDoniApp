let currentImageIndex = 0;
let currentImages = [];

document.addEventListener('DOMContentLoaded', function () {
    const thumbnails = document.querySelectorAll('.gallery-img');
    console.log("Images found:", thumbnails.length);

    thumbnails.forEach((img) => {
        img.addEventListener('click', () => {
            const index = parseInt(img.dataset.index);
            const carouselId = img.dataset.carouselId;

            console.log("Clicked image index:", index, "carousel:", carouselId);
            const images = window.imageUrls?.[carouselId];

            if (images && images.length > 0) {
                currentImages = images;
                openFullscreen(index);
            } else {
                console.warn("Images not found for carousel:", carouselId);
            }
        });
    });

    document.addEventListener('keydown', function (e) {
        if (e.key === "Escape") closeFullscreen();
        if (e.key === "ArrowLeft") navigateFullscreen(-1);
        if (e.key === "ArrowRight") navigateFullscreen(1);
    });
});

function openFullscreen(index) {
    currentImageIndex = index;
    document.getElementById('fullscreenImage').src = currentImages[index];
    document.getElementById('fullscreenModal').classList.add('active');
    document.body.style.overflow = 'hidden';
}

function closeFullscreen() {
    document.getElementById('fullscreenModal').classList.remove('active');
    document.body.style.overflow = 'auto';
}

function navigateFullscreen(direction) {
    if (!currentImages.length) return;
    currentImageIndex = (currentImageIndex + direction + currentImages.length) % currentImages.length;
    document.getElementById('fullscreenImage').src = currentImages[currentImageIndex];
}
