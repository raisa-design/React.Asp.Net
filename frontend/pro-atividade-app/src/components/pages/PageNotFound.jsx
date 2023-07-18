import React from 'react';
import ImageCoruja from '../../Assets/_120420223 (15).png'

export default function PageNotFound() {
  return (
    <div>
      <h1 className='mt-4 mb-4  text-center'>
            Ops...
            <br/>
            <br/>
            <br/>
            Not Found...
            <br/>
            <img width='150px' height='150px' src={ImageCoruja} alt="" />
            <br/>
            Página não encontrada

           
      </h1>
    </div>
  );
}
