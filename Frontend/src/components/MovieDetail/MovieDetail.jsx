import { useState, useEffect, useRef } from "react";
import { useCart } from "../../context/CartContext";

import MovieFooter from "../MovieFooter/MovieFooter";
import MovieDetailActorList from "./MovieDetailActorList";
import movieNotFound from '/assets/img/movie-not-found.svg';
import MovieDetailTrailer from "./MovieDetailTrailer";




const MovieDetail = ({ id, titulo, imagen, backdrop_path = "../../../public/assets/img/a.webp", sinopsis, duración, idGs, anno, nacionalidad, idAs, trailer }) => {

    // Imagenes
    const posterPath = imagen ? `https://image.tmdb.org/t/p/original/${imagen}` : movieNotFound;
    const backdropPath = `${backdrop_path}`;
    const backgroundStyle = {
        backgroundImage: `url(${backdropPath})`
    }

    const [trailerPath, setTrailerPath] = useState('');

    useEffect(() => {

        const trailerPath = trailer

        trailerPath && setTrailerPath(trailerPath);
    }, []);

    // Manejo de carrito
    const { addTicket } = useCart();

    const addToCart = (screening, cantidad) => {
        flyToCart();

        setTimeout(() => {
            const movie = { id, titulo, sinopsis, imagen, duración, idGs, anno, nacionalidad, idAs, trailer };
            addTicket(movie, screening.id, cantidad);
        }, 2000);
    }

    // Efecto de que la película vuela al carrito
    const imgRef = useRef();
    const movieCardRef = useRef();
    const { cartWidgetRef } = useCart();

    const flyToCart = (e) => {
        setTimeout(() => {
            // Setteo al carrito la clase active para que se corra el carrito
            cartWidgetRef.current.classList.add('cartWidget_active');
        }, 1300);

        // Hago una copia del elemento imagen y lo agrego al movieDetail
        let flyingImg = imgRef.current.cloneNode();
        flyingImg.classList.add('flyingImg');
        movieCardRef.current.appendChild(flyingImg);

        // Encuentro las posiciones
        const flyingImg_pos = flyingImg.getBoundingClientRect();
        const cartWidget_pos = cartWidgetRef.current.getBoundingClientRect();
        let data = {
            left: cartWidget_pos.left - (cartWidget_pos.width / 2 + flyingImg_pos.left + flyingImg_pos.width / 2),
            bottom: flyingImg_pos.bottom - flyingImg_pos.width + cartWidget_pos.bottom / 2,
        }

        flyingImg.style.cssText = `
            --left: ${data.left.toFixed(2)}px;
            --bottom: ${data.bottom.toFixed(2)}px;
        `

        setTimeout(() => {
            movieCardRef.current && movieCardRef.current.removeChild(flyingImg);
            cartWidgetRef.current && cartWidgetRef.current.classList.remove('cartWidget_active');
            cartWidgetRef.current && cartWidgetRef.current.classList.add('cartWidget_shakeCount');
        }, 2000);

        setTimeout(() => {
            cartWidgetRef.current.classList.remove('cartWidget_shakeCount');
        }, 2500);
    }

    return (
        <div>
            <article className="movieDetailCard" ref={movieCardRef} >
                <div className='movieDetailCard-header'>
                    <div className='movieDetailCard-header_background' style={backgroundStyle}></div>
                </div>

                <div className='movieDetailCard-body'>
                    <div className='movieDetailCard-body_left'>

                        <button className='movieDetailCard-body_left_poster'>
                            <img src={posterPath} alt={`Póster de la película ${titulo}`} className={!imagen ? 'movieDetailCard-body_left_poster_notFound' : ''} ref={imgRef} />

                            <MovieDetailTrailer trailerPath={trailerPath} />
                        </button>

                        <ul className='movieDetailCard-body_left_details rounded-xl tracking-wider font-quicksand text-xs sm:text-sm md:text-base'>
                            <li>
                                <span className='underline  font-semibold'>Fecha de estreno</span>
                                <span className='font-[500]'> {anno}</span>
                            </li>

                            <li>
                                <span className='underline  font-semibold'>Nacionalidad</span>
                                <span className='font-[500]'> {nacionalidad}</span>
                            </li>
                            <li>
                                <span className='underline font-semibold'>Duración</span>
                                <span className='font-[500]'>{duración ? `${duración} Minutos` : 'SIN DATOS'}</span>
                            </li>
                        </ul>
                    </div>

                    <div className='movieDetailCard-body_right'>
                        <div className="movieDetailCard-body_right_top">
                            <div className='movieDetailCard-body_right_top_titulos rounded-lg'>
                                <h1 className='text-lg xs:text-base sm:text-xl md:text-2xl lg:text-4xl font-[400]'>{titulo}</h1>
                                {/* <h2 className='text-sm xs:text-sm sm:text-base md:text-lg lg:text-xl font-[300] italic'>{sinopsis}</h2> */}
                            </div>

                            <ul className='movieDetailCard-body_right_top_idGs'>
                                {idGs && idGs.map((g) => (
                                    <li className='badge badge-lg badge-primary text-[0.7rem] sm:text-xs md:text-sm lg:text-lg' key={g.id}>{g.name}</li>
                                ))}
                            </ul>
                        </div>


                        <p className='tracking-wider flex flex-col gap-2 min-h-[200px]'>
                            <span className='text-2xl xs:text-lg sm:text-2xl md:text-3xl lg:text-4xl'>Sinopsis</span>
                            <span className='text-base xs:text-xs sm:text-sm md:text-base lg:text-lg'>{sinopsis || 'Sin datos sobre la sinopsis de esta película'}</span>
                        </p>

                        <div className='movieDetailCard-body_right_idAs'>
                            <h3 className='text-2xl xs:text-lg sm:text-2xl md:text-3xl lg:text-3xl tracking-wider'>Reparto principal</h3>

                            <MovieDetailActorList idAs={idAs} />
                        </div>
                    </div>

                    <div className="movieDetailCard-body_bottom">
                        <MovieFooter onAdd={addToCart} submitText='Agregar a mis entradas' movieId={id} />
                    </div>
                </div>
            </article>
        </div>
    )
}

export default MovieDetail;
