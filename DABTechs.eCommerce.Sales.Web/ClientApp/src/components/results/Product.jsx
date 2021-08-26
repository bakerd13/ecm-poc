import React from "react";
import Title from "./components/Title";
import ItemImage from "./components/itemImage";
import ProductCode from "./components/ProductCode";
import ProductDescription from "./components/ProductDescription";
import ProductSizes from "./components/ProductSizes";
import ProductPriceHistory from "./components/ProductPriceHistory";
import ProductSelector from "./components/ProductSelector";

import "./Product.css";

class Product extends React.Component {

    render() {
        
        return (
            <article className="product-article">
                <section className="item">
                    <ItemImage item={this.props.item} />
                    <div className="product-info-container">
                        <div className="product-desc-container">
                            <Title item={this.props.item} />
                            <ProductCode item={this.props.item} />
                            <ProductDescription item={this.props.item} />
                            <ProductSizes item={this.props.item} />
                        </div>
                        <div className="product-price-container">
                            <ProductPriceHistory item={this.props.item} />
                        </div>
                    </div>
                    <div>
                        <ProductSelector item={this.props.item} />
                    </div>
                </section>
            </article>
                );
            }
        }
        
export default Product;