// import React, { useEffect, useState } from "react";
// import axios from '../../api/axios';
// import { Link } from "react-router-dom";

// const MovieCarouselItem = ({ slide, stopSlide, startSlide }) => {
//     const [imgURL, setImgURL] = useState('');

//     // Ajusta esta URL base a la dirección de tu API


//     useEffect(() => {
//         // Asumiendo que tu backend tiene un endpoint similar para obtener imágenes por ID de película
//         axios.get(`/movies/${slide.id}/images`)
//             .then(res => {
//                 // Ajusta según la estructura de respuesta de tu backend
//                 // Por ejemplo, supongamos que tu API retorna la URL de la imagen directamente
//                 setImgURL(res.data.file_path);
//             })
//             .catch(err => console.log(err));
//     }, [slide.id]);

//     return (
//         <Link to={`/movie/${slide.id}`} className="movieCarousel-item" onMouseEnter={stopSlide} onMouseOut={startSlide}>
//             <img src={imgURL} alt={`Película - ${slide.title}`} />
//             <div className="movieCarousel-item-desc text-xs pr-2 py-2 sm:text-2xl sm:py-4 sm:pr-5 lg:text-3xl lg:py-6 lg:pr-14">
//                 <h3 className="uppercase whitespace-normal text-right">{slide.title}</h3>
//                 <span className="uppercase">Más info</span>
//             </div>
//         </Link>
//     );
// }

// export default MovieCarouselItem;


import React from "react";
import { Link } from "react-router-dom";

// Datos simulados para un elemento del carrusel
const mockSlideData = {
  id: "movie123",
  title: "Película Simulada",
  imagePath: "/path-to-image.jpg", // Path a una imagen de ejemplo
};

const MovieCarouselItem = ({ slide = mockSlideData, stopSlide, startSlide }) => {
  return (
    <Link to={`/movie/${slide.id}`} className="movieCarousel-item" onMouseEnter={stopSlide} onMouseLeave={startSlide}>
      <img src={`https://image.tmdb.org/t/p/original/${slide.imagePath}`} alt={`Película - ${slide.title}`} />
      <div className="movieCarousel-item-desc text-xs pr-2 py-2 sm:text-2xl sm:py-4 sm:pr-5 lg:text-3xl lg:py-6 lg:pr-14">
        <h3 className="uppercase whitespace-normal text-right">{slide.title}</h3>
        <span className="uppercase">Más info</span>
      </div>
    </Link>
  );
};

export default MovieCarouselItem;
