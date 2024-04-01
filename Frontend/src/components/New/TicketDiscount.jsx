import React from 'react';
import './TicketDiscount.css';

const TicketDiscount = ({ ticketName, porcentajeDiscount }) => {
  return (
    <div className="ticket-discount">
       <span className="ticket">{ticketName}</span>
       
       <span className="descuento">{porcentajeDiscount}</span>
       
    </div>
  );
};

export default TicketDiscount;
