
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { RatingApiServiceService } from 'src/app/services/rating-api-service.service';
import { IRating } from 'src/Model/i-rating';
import { IProductsPages } from 'src/Model/i-products-pages';
import { ProductsPagesService } from 'src/app/services/products-pages.service';
import { IProduct } from 'src/Model/i-product';

// type HoverType = 'quality' | 'price' | 'value'  ;

@Component({
  selector: 'app-rating-product',
  templateUrl: './rating-product.component.html',
  styleUrls: ['./rating-product.component.css']
})

export class RatingProductComponent implements OnInit{

  

  @Input() productId: number | undefined;
  @Input() productName: string | undefined;

  nickname:string='';
  quality:number=0
  price:number=0;
  value:number=0;
  review: string='';

  product: IProduct | undefined=undefined;
  prodId:number=0;
  constructor(private ratingApiService: RatingApiServiceService , private productApiPages: ProductsPagesService) {}
  ngOnInit(): void {
    
    this.productApiPages.getproductsById(this.prodId).subscribe(data => {
      this.product = data;
      console.log(this.product);
    });  }

  submitRating(): void {
    if (this.productId !== undefined , this.productName!== undefined ) {
      const newRating: IRating =
      {
        // productId: this.productId,
        nickname:this.nickname,
        review: this.review,
        quality:this.quality,
        value:this.value,
        price:this.price,
        productName:this.productName,
      };

      this.ratingApiService.ratingProduct(newRating).subscribe(
        () => {
          alert('Your rating has been saved successfully ... Thank you for your rating');
          this.review = '';
        },
        (error) => {
          console.error('Failed to save rating', error);
          alert('An error occurred while saving the Rating ... Please try again.');
        }
      );
    } else {
      alert('Invalid productId.'); 
    }
  }


  @Input() rating: number | undefined = 0;
  @Input() starCount: number | undefined = 5;
  @Input() color: string | undefined = 'gold';
  @Input() onStarClick: ((rating: number) => void) | undefined;
  

  onClick(rating: number, type: string): void {
    if (type === 'quality') {
      this.quality = rating;
    } else if (type === 'price') {
      this.price = rating;
    } else if (type === 'value') {
      this.value = rating;
    }
  }

  

  // hoverQuality: number = 0;
  // hoverPrice: number = 0;
  // hoverValue: number = 0;
  // currentHoverType: HoverType | null = null;

  // onHover(rating: number, type: HoverType): void {
  //   if (rating !== this[type] && this.currentHoverType !== type) {
  //     this.currentHoverType = type;
  //     if (type === 'quality') {
  //       this.hoverQuality = rating;
  //       this.hoverPrice = 0;
  //       this.hoverValue = 0;
  //     } else if (type === 'price') {
  //       this.hoverQuality = 0;
  //       this.hoverPrice = rating;
  //       this.hoverValue = 0;
  //     } else if (type === 'value') {
  //       this.hoverQuality = 0;
  //       this.hoverPrice = 0;
  //       this.hoverValue = rating;
  //     }
  //   }
  // }



}



