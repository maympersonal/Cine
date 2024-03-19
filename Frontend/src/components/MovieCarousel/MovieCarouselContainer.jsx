import React, { useEffect, useState } from "react";
import axios from '../../api/axios';
import MovieCarousel from "./MovieCarousel";

const MovieCarouselContainer = () => {
    const [slides, setSlides] = useState([]);



    useEffect(() => {
        // Sustituye la URL por la de tu backend .NET
        axios.get(`/movies/now_playing`)
            .then(res => {
                // Asume que tu API devuelve los resultados en una estructura similar,
                // ajusta esto según sea necesario.
                setSlides(res.data.slice(0,15));
            })
            .catch(err => console.log(err));
    }, []);

    return (
        <div>
            {slides && slides.length > 0 &&
                <MovieCarousel slides={slides} controls indicators/>
            }
        </div>
    );
}

export default MovieCarouselContainer;
